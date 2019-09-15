using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    class Program
    {
        public static IBLEmployees blHandler;

        static void Main(string[] args)
        {
            SetupDependencies();
            SetupService();
        }

        private static void SetupDependencies()
        {
            blHandler = new BLEmployees(new DataAccessLayer.DALEmployeesMongo());
        }

        private static void SetupService()
        {
            BasicHttpBinding binding = new BasicHttpBinding();
            binding.Name = "binding1";
            binding.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard;
            binding.Security.Mode = BasicHttpSecurityMode.None;

            Uri baseAddress = new Uri("http://localhost:8834/tsi1/");
            Uri address = new Uri("http://localhost:8834/tsi1/employee");
            ServiceHost serviceHost = new ServiceHost(typeof(ServiceEmployees), baseAddress);

            serviceHost.AddServiceEndpoint(typeof(IServiceEmployees), binding, address);
            serviceHost.Open();

            Console.WriteLine("The service is ready.");
            Console.WriteLine("Press <ENTER> to terminate service.");
            Console.WriteLine();
            Console.ReadLine();

            serviceHost.Close();
        }
    }
}
