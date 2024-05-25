import React from "react";
import { useMsal } from "@azure/msal-react";
import { loginRequest } from "../authConfig";
import Button from "react-bootstrap/Button";

function handleSigninPopup(instance) {
    instance.loginPopup(loginRequest).catch(e => {
        console.error(e);
    });
}

function handleSigninRedirect(instance) {
    instance.loginRedirect(loginRequest).catch(e => {
        console.error(e);
    });
}

/**
 * Renders a button which, when selected, will open a popup for login
 */
export const SignInButton = () => {
    const { instance } = useMsal();

    return (
        <div>
            <Button variant="secondary" className="ml-auto" onClick={() => handleSigninPopup(instance)}>Sign in using Popup</Button>
            <br />
            <Button variant="secondary" className="ml-auto" onClick={() => handleSigninRedirect(instance)}>Sign in using Redirect</Button>
        </div>
    );
}