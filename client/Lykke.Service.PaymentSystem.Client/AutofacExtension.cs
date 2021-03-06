﻿using System;
using Autofac;
using Common.Log;

namespace Lykke.Service.PaymentSystem.Client
{
    public static class AutofacExtension
    {
        public static void RegisterPaymentSystemClient(this ContainerBuilder builder, string serviceUrl, ILog log)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            if (log == null) throw new ArgumentNullException(nameof(log));
            if (string.IsNullOrWhiteSpace(serviceUrl))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(serviceUrl));

            builder.RegisterType<PaymentSystemClient>()
                .WithParameter("serviceUrl", serviceUrl)
                .As<IPaymentSystemClient>()
                .SingleInstance();
        }

        public static void RegisterPaymentSystemClient(this ContainerBuilder builder, PaymentSystemServiceClientSettings settings, ILog log)
        {
            builder.RegisterPaymentSystemClient(settings?.ServiceUrl, log);
        }
    }
}
