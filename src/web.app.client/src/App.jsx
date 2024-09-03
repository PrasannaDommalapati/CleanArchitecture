import "./App.css";
import { BrowserRouter as Router, Route, Link } from "react-router-dom";
import NavMenu from "./components/NavMenu";

const App = () => {
    return <Router>
        <NavMenu />

        {/*<Route path="/products" exact component={PageProducts} />*/}
    </Router>
}

export default App;
