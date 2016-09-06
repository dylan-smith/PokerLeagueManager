// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.
//
// To add a suppression to this file, right-click the message in the
// Code Analysis results, point to "Suppress Message", and click
// "In Suppression File".
// You do not need to add suppressions to this file manually.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "DTO", Justification = "I like the assembly name like this")]

[assembly: SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "DTO", Scope = "namespace", Target = "PokerLeagueManager.Common.Infrastructure", Justification = "Namespace should match assembly name")]

[assembly: SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "DTO", Scope = "namespace", Target = "PokerLeagueManager.Common.DTO", Justification = "Namespace should match assembly name")]

[assembly: SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "DTO", Scope = "namespace", Target = "PokerLeagueManager.Common.DTO", Justification = "Namespace should match assembly name")]

[assembly: SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "DTO", Scope = "namespace", Target = "PokerLeagueManager.Common.DTO.Lookups", Justification = "Namespace should match assembly name")]
