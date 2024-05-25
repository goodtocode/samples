
# Quick-Start with Create-React-App, Azure Active Directory B2C and MSAL
## Procedure
Follow the links here to recreate this app
### [Create React SPA](https://docs.microsoft.com/en-us/azure/active-directory/develop/tutorial-v2-react)
```node.js npx/npm
npx create-react-app msal-react-tutorial # Create a new React app
cd msal-react-tutorial # Change to the app directory
npm install @azure/msal-browser @azure/msal-react # Install the MSAL packages
npm install react-bootstrap bootstrap # Install Bootstrap for styling
```
### [Configure SPA](https://docs.microsoft.com/en-us/azure/active-directory/develop/tutorial-v2-react)

| Setting       | Value       | Description  |
| ------------- | ------------- | ----- |
| Enter_the_Application_Id_Here | CLIENT_ID (guid) | The Application (client) ID of the application you registered |
| Enter_the_Cloud_Instance_Id_Here | https://login.microsoftonline.com | The Azure cloud instance in which your application is registered. For the main (or global) Azure cloud, enter https://login.microsoftonline.com. For national clouds (for example, China), you can find appropriate values in National clouds. |
| Enter_the_Tenant_Info_Here | TENANT_ID or TENANT_NAME  | Set to one of the following options: If your application supports accounts in this organizational directory, replace this value with the directory (tenant) ID or tenant name (for example, contoso.microsoft.com). If your application supports accounts in any organizational directory, replace this value with organizations. If your application supports accounts in any organizational directory and personal Microsoft accounts, replace this value with common. To restrict support to personal Microsoft accounts only, replace this value with consumers. |
| Enter_the_Redirect_Uri_Here | http://localhost:3000 or https://172.0.0.1:5500 | URL of your local running app, with port number |
| Enter_the_Graph_Endpoint_Here | https://graph.microsoft.com | The instance of the Microsoft Graph API the application should communicate with. For the global Microsoft Graph API endpoint, replace both instances of this string with https://graph.microsoft.com. For endpoints in national cloud deployments, see National cloud deployments in the Microsoft Graph documentation. |

### [AAD App Registration](https://docs.microsoft.com/en-us/azure/active-directory/develop/scenario-spa-app-registration)
* Sign in to the Azure portal.
* If you have access to multiple tenants, use the Directories + subscriptions filter  in the top menu to select the tenant in which you want to register an application.
* Search for and select Azure Active Directory.
* Under Manage, select App registrations > New registration.
* Enter a Name for your application. Users of your app might see this name, and you can change it later.
* Choose the Supported account types for the application. Do NOT enter a Redirect URI. For a description of the different account types, see the Register an application.
* Select Register to create the app registration.
* Under Authentication > Add a platform.
* Under Web applications, select the Single-page application tile.
* Under Redirect URIs, enter a redirect URI. Do NOT select either checkbox under Implicit grant and hybrid flows.
* Select Configure to finish adding the redirect URI.

## Scripts
### `npm start`
Runs the app in the development mode.\
Open [http://localhost:3000](http://localhost:3000) to view it in your browser.
The page will reload when you make changes.\
You may also see any lint errors in the console.

### `npm test`
Launches the test runner in the interactive watch mode.\
See the section about [running tests](https://facebook.github.io/create-react-app/docs/running-tests) for more information.

### `npm run build`
Builds the app for production to the `build` folder.\
It correctly bundles React in production mode and optimizes the build for the best performance.
The build is minified and the filenames include the hashes.\
Your app is ready to be deployed!
See the section about [deployment](https://facebook.github.io/create-react-app/docs/deployment) for more information.

### `npm run eject`
**Note: this is a one-way operation. Once you `eject`, you can't go back!**
If you aren't satisfied with the build tool and configuration choices, you can `eject` at any time. This command will remove the single build dependency from your project.
Instead, it will copy all the configuration files and the transitive dependencies (webpack, Babel, ESLint, etc) right into your project so you have full control over them. All of the commands except `eject` will still work, but they will point to the copied scripts so you can tweak them. At this point you're on your own.
You don't have to ever use `eject`. The curated feature set is suitable for small and middle deployments, and you shouldn't feel obligated to use this feature. However we understand that this tool wouldn't be useful if you couldn't customize it when you are ready for it.

# Technology
* [Create React App](https://github.com/facebook/create-react-app)
* [Create React App](https://github.com/facebook/create-react-app)


## Learn More - Create React App
You can learn more in the [Create React App documentation](https://facebook.github.io/create-react-app/docs/getting-started).
To learn React, check out the [React documentation](https://reactjs.org/).
### Code Splitting
This section has moved here: [https://facebook.github.io/create-react-app/docs/code-splitting](https://facebook.github.io/create-react-app/docs/code-splitting)
### Analyzing the Bundle Size
This section has moved here: [https://facebook.github.io/create-react-app/docs/analyzing-the-bundle-size](https://facebook.github.io/create-react-app/docs/analyzing-the-bundle-size)
### Making a Progressive Web App
This section has moved here: [https://facebook.github.io/create-react-app/docs/making-a-progressive-web-app](https://facebook.github.io/create-react-app/docs/making-a-progressive-web-app)
### Advanced Configuration
This section has moved here: [https://facebook.github.io/create-react-app/docs/advanced-configuration](https://facebook.github.io/create-react-app/docs/advanced-configuration)
### Deployment
This section has moved here: [https://facebook.github.io/create-react-app/docs/deployment](https://facebook.github.io/create-react-app/docs/deployment)
### `npm run build` fails to minify
This section has moved here: [https://facebook.github.io/create-react-app/docs/troubleshooting#npm-run-build-fails-to-minify](https://facebook.github.io/create-react-app/docs/troubleshooting#npm-run-build-fails-to-minify)