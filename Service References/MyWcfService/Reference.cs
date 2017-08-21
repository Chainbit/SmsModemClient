﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using System;

namespace SmsModemClient.MyWcfService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="MyWcfService.IManager")]
    public interface IManager {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IManager/TestConnection", ReplyAction="http://tempuri.org/IManager/TestConnectionResponse")]
        SmsModemClient.MyWcfService.TestConnectionResponse TestConnection(SmsModemClient.MyWcfService.TestConnectionRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IManager/TestConnection", ReplyAction="http://tempuri.org/IManager/TestConnectionResponse")]
        System.Threading.Tasks.Task<SmsModemClient.MyWcfService.TestConnectionResponse> TestConnectionAsync(SmsModemClient.MyWcfService.TestConnectionRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IManager/GetCommand", ReplyAction="http://tempuri.org/IManager/GetCommandResponse")]
        SmsModemClient.MyWcfService.GetCommandResponse GetCommand(SmsModemClient.MyWcfService.GetCommandRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IManager/GetCommand", ReplyAction="http://tempuri.org/IManager/GetCommandResponse")]
        System.Threading.Tasks.Task<SmsModemClient.MyWcfService.GetCommandResponse> GetCommandAsync(SmsModemClient.MyWcfService.GetCommandRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IManager/SendDataToServer", ReplyAction="http://tempuri.org/IManager/SendDataToServerResponse")]
        SmsModemClient.MyWcfService.SendDataToServerResponse SendDataToServer(SmsModemClient.MyWcfService.SendDataToServerRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IManager/SendDataToServer", ReplyAction="http://tempuri.org/IManager/SendDataToServerResponse")]
        System.Threading.Tasks.Task<SmsModemClient.MyWcfService.SendDataToServerResponse> SendDataToServerAsync(SmsModemClient.MyWcfService.SendDataToServerRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="TestConnection", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class TestConnectionRequest {
        
        public TestConnectionRequest() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="TestConnectionResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class TestConnectionResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public string TestConnectionResult;
        
        public TestConnectionResponse() {
        }
        
        public TestConnectionResponse(string TestConnectionResult) {
            this.TestConnectionResult = TestConnectionResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetCommand", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class GetCommandRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public string clientID;
        
        public GetCommandRequest() {
        }
        
        public GetCommandRequest(string clientID) {
            this.clientID = clientID;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetCommandResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class GetCommandResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public string GetCommandResult;
        
        public GetCommandResponse() {
        }
        
        public GetCommandResponse(string GetCommandResult) {
            this.GetCommandResult = GetCommandResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="SendDataToServer", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class SendDataToServerRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public string data;
        
        public SendDataToServerRequest() {
        }
        
        public SendDataToServerRequest(string data) {
            this.data = data;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="SendDataToServerResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class SendDataToServerResponse {
        
        public SendDataToServerResponse() {
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IManagerChannel : SmsModemClient.MyWcfService.IManager, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ManagerClient : System.ServiceModel.ClientBase<SmsModemClient.MyWcfService.IManager>, SmsModemClient.MyWcfService.IManager {
        
        public ManagerClient() {
        }
        
        public ManagerClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ManagerClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ManagerClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ManagerClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public SmsModemClient.MyWcfService.TestConnectionResponse TestConnection(SmsModemClient.MyWcfService.TestConnectionRequest request) {
            return base.Channel.TestConnection(request);
        }
        
        public System.Threading.Tasks.Task<SmsModemClient.MyWcfService.TestConnectionResponse> TestConnectionAsync(SmsModemClient.MyWcfService.TestConnectionRequest request) {
            return base.Channel.TestConnectionAsync(request);
        }
        
        public SmsModemClient.MyWcfService.GetCommandResponse GetCommand(SmsModemClient.MyWcfService.GetCommandRequest request) {
            return base.Channel.GetCommand(request);
        }
        
        public System.Threading.Tasks.Task<SmsModemClient.MyWcfService.GetCommandResponse> GetCommandAsync(SmsModemClient.MyWcfService.GetCommandRequest request) {
            return base.Channel.GetCommandAsync(request);
        }
        
        public SmsModemClient.MyWcfService.SendDataToServerResponse SendDataToServer(SmsModemClient.MyWcfService.SendDataToServerRequest request) {
            return base.Channel.SendDataToServer(request);
        }
        
        public System.Threading.Tasks.Task<SmsModemClient.MyWcfService.SendDataToServerResponse> SendDataToServerAsync(SmsModemClient.MyWcfService.SendDataToServerRequest request) {
            return base.Channel.SendDataToServerAsync(request);
        }

        internal object TestConnection()
        {
            throw new NotImplementedException();
        }
    }
}