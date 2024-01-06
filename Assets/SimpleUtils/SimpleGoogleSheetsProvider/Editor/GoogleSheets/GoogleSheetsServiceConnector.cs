using System;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Util.Store;

namespace SimpleUtils.SimpleGoogleSheetsProvider.Editor.GoogleSheets
{
    internal class GoogleSheetsServiceConnector
    {
        private static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        private readonly AuthorizationData _authorizationData;
        private SheetsService _sheetService;
        private IDataStore _dataStore;

        public GoogleSheetsServiceConnector(AuthorizationData authorizationData)
        {
            _authorizationData = authorizationData;
        }

        public async Task<SheetsService> Connect()
        {
            if (_sheetService != null)
                return _sheetService;
            
            ClientSecrets clientSecrets = new ClientSecrets
            {
                ClientId = _authorizationData.ClientId,
                ClientSecret = _authorizationData.ClientSecret
            };
            
            CancellationTokenSource cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
            var dataStore = _dataStore ?? new FileDataStore($"Library/Google/SimpleGoogleSheetsProvider", true);
            
            var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                clientSecrets,
                Scopes,
                _authorizationData.ClientId,
                cts.Token,
                dataStore);
            
            return _sheetService = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential
            });
        }
    }
}