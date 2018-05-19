using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.Practices.Unity.ServiceLocatorAdapter;
using Microsoft.Practices.ServiceLocation;
using System.Configuration;
using Common;
using EmailService.Process.SubscriptionService;
using EmailService.Services;

namespace EmailService.Process
{
    class Program
    {
        private static global::Common.SubscriberServiceHost mHost;
        private const String cAddress = "net.msmq://localhost/private/EmailQueueTransacted";
        private const String cMexAddress = "net.tcp://localhost:9018/EmailQueueTransacted/mex";

        static void Main(string[] args)
        {
            ResolveDependencies();
            HostSubscriberService();
            SubscribeForEvents();
            using (ServiceHost lHost = new ServiceHost(typeof(EmailService.Services.EmailService)))
            {
                lHost.Open();
                Console.WriteLine("Email Service Started");
                while (Console.ReadKey().Key != ConsoleKey.Q) ;
            }
        }

        private static void HostSubscriberService()
        {
            mHost = new SubscriberServiceHost(typeof(SubscriberService), cAddress, cMexAddress, true, ".\\private$\\EmailQueueTransacted");
        }

        private static void ResolveDependencies()
        {

            UnityContainer lContainer = new UnityContainer();
            UnityConfigurationSection lSection
                    = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            lSection.Containers["containerOne"].Configure(lContainer);
            UnityServiceLocator locator = new UnityServiceLocator(lContainer);
            ServiceLocator.SetLocatorProvider(() => locator);
        }

        private static void SubscribeForEvents()
        {
            SubscriptionServiceClient lClient = new SubscriptionServiceClient();
            lClient.Subscribe("SendEmail", cAddress);
        }
    }
}
