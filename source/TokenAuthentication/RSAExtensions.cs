﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Xml;

namespace mxProject.TokenAuthentication
{

    /// <summary>
    /// Extension methods for <see cref="RSA"/>.
    /// </summary>
    public static class RSAExtensions
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rsa"></param>
        /// <param name="xmlString"></param>
        public static void FromXmlStringEx(this RSA rsa, string xmlString)
        {
            RSAParameters parameters = new RSAParameters();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);

            if (xmlDoc.DocumentElement.Name.Equals("RSAKeyValue"))
            {
                foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
                {
                    switch (node.Name)
                    {
                        case "Modulus": parameters.Modulus = Convert.FromBase64String(node.InnerText); break;
                        case "Exponent": parameters.Exponent = Convert.FromBase64String(node.InnerText); break;
                        case "P": parameters.P = Convert.FromBase64String(node.InnerText); break;
                        case "Q": parameters.Q = Convert.FromBase64String(node.InnerText); break;
                        case "DP": parameters.DP = Convert.FromBase64String(node.InnerText); break;
                        case "DQ": parameters.DQ = Convert.FromBase64String(node.InnerText); break;
                        case "InverseQ": parameters.InverseQ = Convert.FromBase64String(node.InnerText); break;
                        case "D": parameters.D = Convert.FromBase64String(node.InnerText); break;
                    }
                }
            }
            else
            {
                throw new Exception("Invalid XML RSA key.");
            }

            rsa.ImportParameters(parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rsa"></param>
        /// <param name="includePrivateParameters"></param>
        /// <returns></returns>
        public static string ToXmlStringEx(this RSA rsa, bool includePrivateParameters = false)
        {
            RSAParameters parameters = rsa.ExportParameters(includePrivateParameters);

            if (includePrivateParameters)
            {
                return string.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent><P>{2}</P><Q>{3}</Q><DP>{4}</DP><DQ>{5}</DQ><InverseQ>{6}</InverseQ><D>{7}</D></RSAKeyValue>"
                    , Convert.ToBase64String(parameters.Modulus)
                    , Convert.ToBase64String(parameters.Exponent)
                    , Convert.ToBase64String(parameters.P)
                    , Convert.ToBase64String(parameters.Q)
                    , Convert.ToBase64String(parameters.DP)
                    , Convert.ToBase64String(parameters.DQ)
                    , Convert.ToBase64String(parameters.InverseQ)
                    , Convert.ToBase64String(parameters.D)
                    );
            }
            else
            {
                return string.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent></RSAKeyValue>"
                    , Convert.ToBase64String(parameters.Modulus)
                    , Convert.ToBase64String(parameters.Exponent)
                    );
            }
        }

    }

}
