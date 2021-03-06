﻿// Copyright 2007-2017 Chris Patterson, Dru Sellers, Travis Smith, et. al.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace MassTransit.SimpleInjectorIntegration
{
    using System.Threading.Tasks;
    using GreenPipes;
    using Scoping;
    using SimpleInjector;


    public class SimpleInjectorConsumerFactory<TConsumer> :
        IConsumerFactory<TConsumer>
        where TConsumer : class
    {
        readonly IConsumerFactory<TConsumer> _factory;

        public SimpleInjectorConsumerFactory(Container container)
        {
            _factory = new ScopeConsumerFactory<TConsumer>(new SimpleInjectorConsumerScopeProvider(container));
        }

        Task IConsumerFactory<TConsumer>.Send<T>(ConsumeContext<T> context, IPipe<ConsumerConsumeContext<TConsumer, T>> next)
        {
            return _factory.Send(context, next);
        }

        void IProbeSite.Probe(ProbeContext context)
        {
            _factory.Probe(context);
        }
    }
}