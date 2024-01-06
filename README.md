# SimpleGoogleSheetsProvider
 Provides a simple way to load and unload rows from google tables.
## How to use
- Copy the SimpleUtils folder into your project.
- Add the GoogleSheetsPullMethod attribute to the method that will accept values from the table.
- Add the GoogleSheetsPushMethod attribute to the method that will pass values to the table.
- In the inspector, click on the buttons that appear.
> [!NOTE]
> Attributes take 2 values: the first one is spreadsheetID, which can be found in the address bar of the table after the **spreadsheets/d/** symbols and the second is the **index** of the table.
> Method that will accept values must contain a **GoogleSheetValues** input parameter. And the method that sends the values must return **GoogleSheetValues**.
## Before starting work
You have to be logged in to use google tables. You need your ClientID and ClientSecret to authorize. How to get them will be described below. In the Unity editor click **Tools/Simple utils/Simple google sheets provider** and enter these values in the window that appears.
## How to get ClientID and ClientSecret
- Visit [Google API Console](https://console.developers.google.com/) and select the Credentials section under APIs & Services. This opens [Google APIs: Credentials](https://console.cloud.google.com/apis/credentials).
- Select your Google APIs and Services project for your Unity application. If you do not have a Google APIs and Services project yet, select CREATE PROJECT to create one.
- Enable the Google Sheets API support. To do this, go to [Google APIs: Credentials](https://console.cloud.google.com/apis/credentials).
- Select the CREATE CREDENTIALS option and select OAuth Client ID.
