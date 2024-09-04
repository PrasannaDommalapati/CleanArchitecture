import { useState } from "react";
import {
  Collapse,
  Navbar,
  NavbarToggler,
  NavbarBrand,
  Nav,
  NavItem,
  NavLink,
} from "reactstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faRightFromBracket } from "@fortawesome/free-solid-svg-icons";
import logo from "../assets/header-logo.svg";

const CustomNavbar = () => {
  const [isOpen, setIsOpen] = useState(false);

  const toggle = () => setIsOpen(!isOpen);

  return (
    <header>
      <div>
        <Navbar
          className="navbar-expand-sm navbar-toggleable-sm"
          color="light"
          light
        >
          <NavbarBrand href="/">
            <img
              alt="logo"
              src={logo}
              style={{
                height: 100,
                width: 250,
              }}
            />
          </NavbarBrand>
          <NavbarToggler onClick={toggle} className="me-2" />
          <Collapse
            className="d-sm-inline-flex flex-sm-row-reverse"
            isOpen={!isOpen}
            navbar
          >
            <Nav className="ml-auto" navbar>
              {
                <NavItem>
                  <NavLink href="/login">Login</NavLink>
                </NavItem>
              }
              <NavItem>
                <NavLink href="/signup">Register</NavLink>
              </NavItem>
              {
                <NavItem>
                  <NavLink>
                    <FontAwesomeIcon icon={faRightFromBracket} />
                  </NavLink>
                </NavItem>
              }
            </Nav>
          </Collapse>
        </Navbar>
      </div>
    </header>
  );
};

export default CustomNavbar;
