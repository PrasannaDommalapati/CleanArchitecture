import { useState } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faBars, faTimes } from "@fortawesome/free-solid-svg-icons";

const CustomNavbar = () => {
  
  const [isMobileMenuOpen, setIsMobileMenuOpen] = useState(false);
  const toggle = () => setIsMobileMenuOpen(!isMobileMenuOpen);

  return (
    <nav className="navbar">
      <div className="navbar__container">
        <a href="/" id="navbar__logo">
          <i className="fas fa-gem"></i>SYNC WORK
        </a>
        <div className="navbar__toggle" id="mobile-menu" onClick={toggle}>
          <span className="bar">
            <FontAwesomeIcon icon={isMobileMenuOpen ? faTimes : faBars}  size="lg"/>
          </span>
        </div>
        <ul
          className={isMobileMenuOpen ? "navbar__menu active" : "navbar__menu"}
        >
          <li className="navbar__item">
            <a href="/" className="navbar__links">
              Home
            </a>
          </li>
          <li className="navbar__item">
            <a href="/tech.html" className="navbar__links">
              Tech
            </a>
          </li>
          <li className="navbar__item">
            <a href="/" className="navbar__links">
              Products
            </a>
          </li>
          <li className="navbar__btn">
            <a href="/signup" className="button">
              Sign Up
            </a>
          </li>
        </ul>
      </div>
    </nav>
  );
};

export default CustomNavbar;
