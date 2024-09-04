import CustomFooter from "./components/CustomFooter";
import CustomNavbar from "./components/CustomNavbar";
import PropTypes from "prop-types";

const Layout = ({ children }) => {
  return (
    <div>
      <CustomNavbar />
      {children}
      <CustomFooter />
    </div>
  );
};

Layout.prototypes = {
  children: PropTypes.any
};
export default Layout;
