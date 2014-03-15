using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace PokerLeagueManager.Common.Utilities
{
    [AttributeUsage(AttributeTargets.Interface)]
    public sealed class ExceptionMarshallingBehaviorAttribute : Attribute, IServiceBehavior, IEndpointBehavior, IContractBehavior
    {
        #region IContractBehavior Members

        void IContractBehavior.AddBindingParameters(ContractDescription contract, ServiceEndpoint endpoint, BindingParameterCollection parameters)
        {
        }

        void IContractBehavior.ApplyClientBehavior(ContractDescription contract, ServiceEndpoint endpoint, ClientRuntime runtime)
        {
            Debug.WriteLine(string.Format("Applying client ExceptionMarshallingBehavior to contract {0}", contract.ContractType));
            this.ApplyClientBehavior(runtime);
        }

        void IContractBehavior.ApplyDispatchBehavior(ContractDescription contract, ServiceEndpoint endpoint, DispatchRuntime runtime)
        {
            Debug.WriteLine(string.Format("Applying dispatch ExceptionMarshallingBehavior to contract {0}", contract.ContractType.FullName));
            this.ApplyDispatchBehavior(runtime.ChannelDispatcher);
        }

        void IContractBehavior.Validate(ContractDescription contract, ServiceEndpoint endpoint)
        {
        }

        #endregion

        #region IEndpointBehavior Members

        void IEndpointBehavior.AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection parameters)
        {
        }

        void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime runtime)
        {
            Debug.WriteLine(string.Format("Applying client ExceptionMarshallingBehavior to endpoint {0}", endpoint.Address));
            this.ApplyClientBehavior(runtime);
        }

        void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher dispatcher)
        {
            Debug.WriteLine(string.Format("Applying dispatch ExceptionMarshallingBehavior to endpoint {0}", endpoint.Address));
            this.ApplyDispatchBehavior(dispatcher.ChannelDispatcher);
        }

        void IEndpointBehavior.Validate(ServiceEndpoint endpoint)
        {
        }

        #endregion

        #region IServiceBehavior Members

        void IServiceBehavior.AddBindingParameters(ServiceDescription service, ServiceHostBase host, Collection<ServiceEndpoint> endpoints, BindingParameterCollection parameters)
        {
        }

        void IServiceBehavior.ApplyDispatchBehavior(ServiceDescription service, ServiceHostBase host)
        {
            Debug.WriteLine(string.Format("Applying dispatch ExceptionMarshallingBehavior to service {0}", service.ServiceType.FullName));
            foreach (ChannelDispatcher dispatcher in host.ChannelDispatchers)
            {
                this.ApplyDispatchBehavior(dispatcher);
            }
        }

        void IServiceBehavior.Validate(ServiceDescription service, ServiceHostBase host)
        {
        }

        #endregion

        #region Private Members

        private void ApplyClientBehavior(ClientRuntime runtime)
        {
            // Don't add a message inspector if it already exists
            foreach (IClientMessageInspector messageInspector in runtime.MessageInspectors)
            {
                if (messageInspector is ExceptionMarshallingMessageInspector)
                {
                    return;
                }
            }

            runtime.MessageInspectors.Add(new ExceptionMarshallingMessageInspector());
        }

        private void ApplyDispatchBehavior(ChannelDispatcher dispatcher)
        {
            // Don't add an error handler if it already exists
            foreach (IErrorHandler errorHandler in dispatcher.ErrorHandlers)
            {
                if (errorHandler is ExceptionMarshallingErrorHandler)
                {
                    return;
                }
            }

            dispatcher.ErrorHandlers.Add(new ExceptionMarshallingErrorHandler());
        }

        #endregion
    }
}
