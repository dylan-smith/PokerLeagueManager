using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerLeagueManager.Common.Events.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PokerLeagueManager.Commands.Tests.Infrastructure
{
    public static class EventsAssert
    {
        public static void AreEqual(IList<IEvent> expected, IList<IEvent> actual)
        {
            if (expected == null)
            {
                throw new ArgumentNullException("Cannot pass in null for the expected or actual events.", "expected");
            }

            if (actual == null)
            {
                throw new ArgumentNullException("Cannot pass in null for the expected or actual events.", "actual");
            }

            if (expected.Count != actual.Count)
            {
                throw new AssertFailedException("The expected events and actual events do not match.  The lengths of the event lists are not equal.");
            }

            for (int i = 0; i < actual.Count; i++)
            {
                CompareEvents(expected[i], actual[i], i);
            }
        }

        private static void CompareEvents(IEvent expectedEvent, IEvent actualEvent, int i)
        {
            if (expectedEvent.GetType() != actualEvent.GetType())
            {
                throw new AssertFailedException(string.Format("The expected and actual events do not match: The events at index #{0} have different types ({1} vs {2})", i.ToString(), expectedEvent.GetType().Name, actualEvent.GetType().Name));
            }

            List<string> ignoreList = new List<string>();
            ignoreList.Add("EventId");
            ignoreList.Add("Timestamp");

            string propertyName = string.Empty;
            string notMatchMessage = string.Empty;

            if (!AreObjectsEqual(expectedEvent, actualEvent, ref propertyName, ref notMatchMessage, ignoreList.ToArray()))
            {
                throw new AssertFailedException(string.Format("The expected and actual events do not match: The property {0} in both events does not match at index #{1}. {2}", propertyName, i, notMatchMessage));
            }
        }

        public static Guid AnyGuid()
        {
            return Guid.Parse("3D3A9906-B35D-472D-8874-7C7150B62C7C");
        }

        /// <summary>
        /// Compares the properties of two objects of the same type and returns if all properties are equal.
        /// </summary>
        /// <param name="objectA">The first object to compare.</param>
        /// <param name="objectB">The second object to compre.</param>
        /// <param name="ignoreList">A list of property names to ignore from the comparison.</param>
        /// <returns><c>true</c> if all property values are equal, otherwise <c>false</c>.</returns>
        private static bool AreObjectsEqual(object objectA, object objectB, ref string propertyName, ref string notMatchMessage, params string[] ignoreList)
        {
            bool result;

            if (objectA != null && objectB != null)
            {
                Type objectType;

                objectType = objectA.GetType();

                result = true; // assume by default they are equal

                foreach (PropertyInfo propertyInfo in objectType.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanRead && !ignoreList.Contains(p.Name)))
                {
                    propertyName = propertyInfo.Name;

                    object valueA;
                    object valueB;

                    valueA = propertyInfo.GetValue(objectA, null);
                    valueB = propertyInfo.GetValue(objectB, null);

                    if (propertyInfo.PropertyType == typeof(Guid))
                    {
                        if ((Guid)valueA == AnyGuid())
                        {
                            break;
                        }
                    }

                    // if it is a primative type, value type or implements IComparable, just directly try and compare the value
                    if (CanDirectlyCompare(propertyInfo.PropertyType))
                    {
                        if (!AreValuesEqual(valueA, valueB))
                        {
                            result = false;
                            break;
                        }
                    }
                    else
                    {
                        if (propertyInfo.PropertyType == typeof(Dictionary<string, object>))
                        {
                            var dicA = (Dictionary<string, object>)valueA;
                            var dicB = (Dictionary<string, object>)valueB;
                            StringResult notMatch = new StringResult();
                            result = DictionaryEqual(dicA, dicB, ref notMatch);
                            notMatchMessage = notMatch.Result;
                            break;
                        }
                        else
                        {
                            result = false;
                            break;
                        }
                    }
                }
            }
            else
            {
                result = object.Equals(objectA, objectB);
            }

            return result;
        }

        /// <summary>
        /// Determines whether value instances of the specified type can be directly compared.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        ///     <c>true</c> if this value instances of the specified type can be directly compared; otherwise, <c>false</c>.
        /// </returns>
        private static bool CanDirectlyCompare(Type type)
        {
            return typeof(IComparable).IsAssignableFrom(type) || type.IsPrimitive || type.IsValueType;
        }

        /// <summary>
        /// Compares two values and returns if they are the same.
        /// </summary>
        /// <param name="valueA">The first value to compare.</param>
        /// <param name="valueB">The second value to compare.</param>
        /// <returns><c>true</c> if both values match, otherwise <c>false</c>.</returns>
        private static bool AreValuesEqual(object valueA, object valueB)
        {
            bool result;
            IComparable selfValueComparer;

            selfValueComparer = valueA as IComparable;

            if ((valueA == null && valueB != null) || (valueA != null && valueB == null))
            {
                result = false; // one of the values is null
            }
            else if (selfValueComparer != null && selfValueComparer.CompareTo(valueB) != 0)
            {
                result = false; // the comparison using IComparable failed
            }
            else if (!object.Equals(valueA, valueB))
            {
                result = false; // the comparison using Equals failed
            }
            else
            {
                result = true; // match
            }

            return result;
        }

        private static bool DictionaryEqual<TKey, TValue, TResult>(IDictionary<TKey, TValue> first, IDictionary<TKey, TValue> second, ref TResult notMatch)
           where TResult : StringResult
        {
            if (first == second)
            {
                return true;
            }

            if ((first == null) || (second == null))
            {
                notMatch.Result = "Either one of two dictionaries is null";
                return false;
            }

            if (first.Count != second.Count)
            {
                notMatch.Result = "The length of two dictionaries are not equal";
                return false;
            }

            var comparer = EqualityComparer<TValue>.Default;
            int i = 0;
            foreach (KeyValuePair<TKey, TValue> kvp in first)
            {
                i++;
                TValue secondValue;

                if (!second.TryGetValue(kvp.Key, out secondValue))
                {
                    notMatch.Result = "Dictionary item #" + i.ToString() + " doesn't match";
                    return false;
                }

                if (!comparer.Equals(kvp.Value, secondValue))
                {
                    notMatch.Result = "Dictionary item #" + i.ToString() + " doesn't match";
                    return false;
                }
            }

            return true;
        }
    }

    public class StringResult
    {
        public string Result { get; set; }
    }
}
