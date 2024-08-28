
# Social Media System

This application is a simple automated content posting system that posts daily content to a specified Facebook page using the Facebook Graph API. The app is written in C# and is configured via an `appsettings.json` file. Content is generated randomly from a provided text file and is posted at a scheduled time every day.

## Features

- **Automated Content Posting**: Automatically posts content to a Facebook page at a scheduled time.
- **Random Content Generation**: Content is randomly selected from a list of predefined options in a text file.
- **Flexible Configuration**: Configurations such as Facebook access tokens and page IDs are managed through an external `appsettings.json` file.
- **Scheduling**: The app uses a timer to schedule daily posts.

## Prerequisites

- **.NET Core SDK**: Make sure you have the .NET Core SDK installed on your machine.
- **Facebook Developer Account**: You need a Facebook developer account to obtain an access token and page ID.
- **Facebook API Access**: The app uses the Facebook Graph API to post content.

## Configuration

Before running the application, ensure that the `appsettings.json` file is configured with the correct values:

```json
{
  "Facebook": {
    "AppId": "your-facebook-app-id",
    "AppSecret": "your-facebook-app-secret",
    "AccessToken": "your-facebook-access-token",
    "PageId": "your-facebook-page-id"
  }
}

```

### File Structure

- `Program.cs`: The main entry point of the application, responsible for loading configurations, initializing the timer, and posting content to Facebook.
- `appsettings.json`: Contains the configuration settings like the Facebook access token and page ID.
- `content.txt`: A text file containing possible content to be posted. Each line represents a different content option.

### Running the Application

1. **Clone the repository** and navigate to the project directory.
2. **Update `appsettings.json`**: Provide your Facebook access token and page ID in the `appsettings.json` file.
3. **Prepare `content.txt`**: Create or update the `content.txt` file with the content you want to post. Each line should represent one piece of content.
4. **Build and Run**:
   ```sh
   dotnet run
   ```
5. **Monitor**: The application will output logs to the console, indicating when content is posted.

### How It Works

1. **Configuration Loading**: The app loads its configuration from the `appsettings.json` file.
2. **Content Generation**: Content is randomly selected from the `content.txt` file.
3. **Initial Post**: The app makes an immediate test post upon startup.
4. **Scheduled Posting**: The app schedules future posts using a timer, posting content every day at the specified time.

### Customization

- **Posting Schedule**: Modify the `GetIntervalToNextPost` method in `Program.cs` if you want to change the posting time.
- **Content File**: Update the `content.txt` file with new content options as needed.

### Troubleshooting

- **Empty Content File**: Ensure that the `content.txt` file is not empty. The app will throw an exception if the file is empty.
- **Facebook OAuth Exception**: Check that your Facebook access token and page ID are correct if you encounter a `FacebookOAuthException`.

### License

This project is licensed under the MIT License. See the `LICENSE` file for more details.
