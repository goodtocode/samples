import React from "react";
import { useMsal } from "@azure/msal-react";
import Button from "react-bootstrap/Button";

function handleSignoutPopup(instance) {
    instance.logoutPopup().catch(e => {
        console.error(e);
    });
}

function handleSignoutRedirect(instance) {
    instance.logoutRedirect().catch(e => {
        console.error(e);
    });
}

/**
 * Renders a button which, when selected, will open a popup for logout
 */
export const SignOutButton = () => {
    const { instance } = useMsal();

    return (
        <div>
            <Button variant="secondary" className="ml-auto" onClick={() => handleSignoutPopup(instance)}>Sign out using Popup</Button>
            <br />
            <Button variant="secondary" className="ml-auto" onClick={() => handleSignoutRedirect(instance)}>Sign out using Redirect</Button>
        </div>
    );
}