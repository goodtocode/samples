import React from "react";
import Navbar from "react-bootstrap/Navbar";
import Container from 'react-bootstrap/Container';
import { useIsAuthenticated } from "@azure/msal-react";
import { SignInButton } from "./SignInButton";
import { SignOutButton } from "./SignOutButton";

/**
 * Renders the navbar component with a sign-in button if a user is not authenticated
 */
export const PageLayout = (props) => {
    const isAuthenticated = useIsAuthenticated();

    return (
        <>
            <Navbar variant="dark">
                <Container>
                    <Navbar.Brand href="/">MyCo</Navbar.Brand>
                    <Navbar.Toggle />
                    <Navbar.Collapse className="justify-content-end">
                        <Navbar.Text>
                            {isAuthenticated ? <SignOutButton /> : <SignInButton />}
                        </Navbar.Text>
                    </Navbar.Collapse>
                </Container>
            </Navbar>
            <h5><center>Sign in, create an account, view profile and sign-out</center></h5>
            <br />
            <br />
            {props.children}
        </>
    );
};