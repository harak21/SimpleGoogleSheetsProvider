using System;
using UnityEngine;

namespace SimpleUtils.SimpleGoogleSheetsProvider.Editor.GoogleSheets
{
    [Serializable]
    internal class AuthorizationData
    {
        [SerializeField] private string clientId;
        [SerializeField] private string clientSecret;

        public string ClientId => clientId;

        public string ClientSecret => clientSecret;

        public AuthorizationData(string id, string secret)
        {
            clientId = id;
            clientSecret = secret;
        }
    }
}
