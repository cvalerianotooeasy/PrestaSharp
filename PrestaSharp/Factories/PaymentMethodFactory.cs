using Bukimedia.PrestaSharp.Entities;
using Bukimedia.PrestaSharp.Factories;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace Bukimedia.PrestaSharp.Factories
{

    public class PaymentMethodFactory : GenericFactory<PaymentMethods>
    {
        protected override string singularEntityName { get { return "payment_methods"; } }
        protected override string pluralEntityName { get { return "payment_methods"; } }

        public PaymentMethodFactory(string BaseUrl, string Account, string SecretKey, ResposeFormatType ResposeFormatType)
                : base(BaseUrl, Account, SecretKey, ResposeFormatType)
        {
        }
    }
}
