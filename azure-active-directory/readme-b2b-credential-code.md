---
page_type: showcase
urlFragment: resource-server-webapi
languages:
  - csharp  
products:
  - azure
  - azure-active-directory  
  - dotnet
description: "Sign-in a user with the Microsoft Identity Platform in a WPF Desktop application and call an ASP.NET Core Web API"
---
# Sign-in a user with the Microsoft Identity Platform in a WPF Desktop application and call an ASP.NET Core Web API

[![Build status](https://identitydivision.visualstudio.com/IDDP/_apis/build/status/AAD%20Samples/.NET%20client%20samples/active-directory-dotnet-native-aspnetcore-v2)](https://identitydivision.visualstudio.com/IDDP/_build/latest?definitionId=516)


### Table of content
- [About this sample](#about-this-sample)
  - [Overview](#overview)
  - [User experience when using this sample](#user-experience-when-using-this-sample)
- [How to run this sample](#how-to-run-this-sample)
  - [Step 1:  Clone or download this repository](#step-1-clone-or-download-this-repository)
  - [Step 2:  Register the sample application with your Azure Active Directory tenant](#step-2-register-the-sample-application-with-your-azure-active-directory-tenant)
  - [Step 3: Run the sample](#step-3-run-the-sample)

### Overview

This sample presents an ASP.NET core Web API, protected by Azure AD OAuth Bearer Authorization. The Web API is called by a .NET Desktop WPF application.

The .Net application uses the Microsoft Authentication Library [MSAL.NET](https://aka.ms/msal-net) to obtain a JWT [Access Token](https://docs.microsoft.com/azure/active-directory/develop/access-tokens) through the [OAuth 2.0](https://docs.microsoft.com/azure/active-directory/develop/active-directory-protocols-oauth-code) protocol. The access token is sent to the ASP.NET Core Web API, which authorizes the user using the ASP.NET JWT Bearer Authentication middleware.

 This sub-folder contains a Visual Studio solution made of two applications: the desktop application (MyCo.Identity.Client), and the Web API (MyCoIdentityService).

### User Flow

The Web API (MyCoIdentityService) maintains an in-memory collection of to-do items for each authenticated user. Several applications signed-in under the same identity will share the same to-do list.

The WPF application (MyCo.Identity.Client) allows a user to:

- Sign-in. The first time a user signs in, a consent screen is presented where the user consents for the application accessing the MyCoIdentity Service on their behalf.
- When the user has signed-in, the user is presented with a list of to-do items fetched from the Web API for this signed-in identity.
- The user can add more to-do items by clicking on *Add item* button.

Next time a user runs the application, the user is signed-in with the same identity as the WPF application maintains a cache on disk. Users can clear the cache (which will have the effect of them signing out).

## How to run this sample

### Step 1:  Clone this repository and find your scenario in the source code

From your shell or command line:

```Shell
git clone https://github.com/goodtocode/templates.git
```

| Project                                 | Description                                   |
| --------------------------------------- | --------------------------------------------- |
| resource-server-azure-functions/        | Showcase for Azure Functions Credential Code  |
| resource-server-daemon-to-api/          | Showcase for B2B Partner Service/Api w/ Credential Code  |
| resource-server-daemon-to-msgraph/      | Showcase for Credential Code, calling downstream MS Graph API  |
| resource-server-on-behalf-of/           | Showcase for Credential Code, calling downstream API  |
| **resource-server-webapi/**                 | Showcase for Credential Code to API  |
| `readme-b2b-resourceserver-api.md`      | This README file.                          |

This readme.md focusses on: **resource-server-webapi/**

### Step 2:  Register the sample application with your Azure Active Directory tenant

1. cd to showcase\identity\src\resource-server-webapi
1. There are two projects in this sample. Each needs to be separately registered in your Azure AD tenant. To register these projects, you can:
- either follow the steps below for manual registration
- or use PowerShell scripts that:
  - **automatically** creates the Azure AD applications and related objects (passwords, permissions, dependencies) for you. Note that this works for Visual Studio only.
  - modify the Visual Studio projects' configuration files.

Follow the steps below to manually walk through the steps to register and configure the applications.												  
																
#### Choose the Azure AD tenant where you want to create your applications

As a first step you'll need to:

1. Sign in to the [Azure portal](https://portal.azure.com) using your work account.
1. If your account is present in more than one Azure AD tenant, select your profile at the top right corner in the menu on top of the page, and then **switch directory**.
   Change your portal session to the desired Azure AD tenant.

#### Register the service app (COMPANY-PRODUCT-ENVIRONMENT)

1. Navigate to the Microsoft identity platform for developers [App registrations](https://go.microsoft.com/fwlink/?linkid=2083908) page.
1. Select **New registration**.
1. In the **Register an application page** that appears, enter your application's registration information:
   - In the **Name** section, enter a meaningful application name that will be displayed to users of the app, use format: `COMPANY-PRODUCT-ENVIRONMENT`.
   - Under **Supported account types**, select **Accounts in any organizational directory and personal Microsoft accounts (e.g. Skype, Xbox, Outlook.com)**.
1. Select **Register** to create the application.
1. In the app's registration screen, find and note the **Application (client) ID**. You use this value in your app's configuration file(s) later in your code.
1. Select **Save** to save your changes.
1. In the app's registration screen, select the **Expose an API** blade to the left to open the page where you can declare the parameters to expose this app as an API for which client applications can obtain [access tokens](https://docs.microsoft.com/azure/active-directory/develop/access-tokens) for.
The first thing that we need to do is to declare the unique [resource](https://docs.microsoft.com/azure/active-directory/develop/v2-oauth2-auth-code-flow) URI that the clients will be using to obtain access tokens for this API. To declare an resource URI, follow the following steps:
   - Click `Set` next to the **Application ID URI** to generate a URI that is unique for this app.
   - For this sample, accept the proposed Application ID URI (api://{clientId}) by selecting **Save**.
1. All APIs have to publish a minimum of one [scope](https://docs.microsoft.com/azure/active-directory/develop/v2-oauth2-auth-code-flow#request-an-authorization-code) for the client's to obtain an access token successfully. To publish a scope, follow the following steps:
   - Select **Add a scope** button open the **Add a scope** screen and Enter the values as indicated below:
        - For **Scope name**, use `access_as_user`.
        - Select **Admins and users** options for **Who can consent?**
        - For **Admin consent display name** type `Access COMPANY-PRODUCT-ENVIRONMENT`
        - For **Admin consent description** type `Allows the app to access COMPANY-PRODUCT-ENVIRONMENT as the signed-in user.`
        - For **User consent display name** type `Access COMPANY-PRODUCT-ENVIRONMENT`
        - For **User consent description** type `Allow the application to access COMPANY-PRODUCT-ENVIRONMENT on your behalf.`
        - Keep **State** as **Enabled**
        - Click on the **Add scope** button on the bottom to save this scope.

#### Configure the service app (COMPANY-PRODUCT-ENVIRONMENT) to use your app registration

Open the project in your IDE (like Visual Studio) to configure the code.
>In the steps below, "ClientID" is the same as "Application ID" or "AppId".						   					

1. Open the `appsettings.json` file
1. Find the app key `Domain` and replace the existing value with your Azure AD tenant name.
1. Find the app key `TenantId` and replace the existing value with your Azure AD tenant ID.
1. Find the app key `ClientId` and replace the existing value with the application ID (clientId) of the `COMPANY-PRODUCT-ENVIRONMENT` application copied from the Azure portal.

#### Register the client app (MyCo.Identity.Client)

1. Navigate to the Microsoft identity platform for developers [App registrations](https://go.microsoft.com/fwlink/?linkid=2083908) page.
1. Select **New registration**.
1. In the **Register an application page** that appears, enter your application's registration information:
   - In the **Name** section, enter a meaningful application name that will be displayed to users of the app, for example `MyCo.Identity.Client`.
   - Under **Supported account types**, select **Accounts in any organizational directory and personal Microsoft accounts (e.g. Skype, Xbox, Outlook.com)**.
1. Select **Register** to create the application.
1. In the app's registration screen, find and note the **Application (client) ID**. You use this value in your app's configuration file(s) later in your code.
1. In the app's registration screen, select **Authentication** in the menu.
   - If you don't have a platform added, select **Add a platform** and select the **Public client (mobile & desktop)** option.
   - In the **Redirect URIs** | **Suggested Redirect URIs for public clients (mobile, desktop)** section, select **https://login.microsoftonline.com/common/oauth2/nativeclient**
1. Select **Save** to save your changes.
1. In the app's registration screen, click on the **API permissions** blade in the left to open the page where we add access to the APIs that your application needs.
   - Click the **Add a permission** button and then,
   - Ensure that the **My APIs** tab is selected.
   - In the list of APIs, select the API `COMPANY-PRODUCT-ENVIRONMENT`.
   - In the **Delegated permissions** section, select the **access_as_user** in the list. Use the search box if necessary.
   - Click on the **Add permissions** button at the bottom.

#### Configure the  client app (MyCo.Identity.Client) to use your app registration

Open the project in your IDE (like Visual Studio) to configure the code.
>In the steps below, "ClientID" is the same as "Application ID" or "AppId".												  
1. Open the `MyCo.Identity.Client\App.Config` file
1. Find the app key `ida:Tenant` and replace the existing value with your Azure AD tenant name.
1. Find the app key `ida:ClientId` and replace the existing value with the application ID (clientId) of the `MyCo.Identity.Client` application copied from the Azure portal.
1. Find the app key `todo:MyCoIdentityScope` and replace the existing value with Scope.
1. Find the app key `todo:MyCoIdentityBaseAddress` and replace the existing value with the base address of the COMPANY-PRODUCT-ENVIRONMENT project (by default `https://localhost:44351/`).
   
### Step 3: Run the sample

Clean the solution, rebuild the solution, and run it. You might want to go into the solution properties and set both projects as startup projects, with the service project starting first.

When you start the Web API from Visual Studio, depending on the browser you use, you'll get:

- an empty web page
- or an error HTTP 401

This behavior is expected as the browser is not authenticated. The WPF application will be authenticated, so it will be able to access the Web API.

Explore the sample by signing in into the MyCoIdentity client, adding items to the To Do list, removing the user account (clearing the cache), and starting again. As explained, if you stop the application without removing the user account, the next time you run the application, you won't be prompted to sign in again. That is because the sample implements a persistent cache for MSAL, and remembers the tokens from the previous run.

NOTE: Remember, the To-Do list is stored in memory in this `MyCoIdentityService` sample. Each time you run the MyCoIdentityService API, your To-Do list will get emptied.