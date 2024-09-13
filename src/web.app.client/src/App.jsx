import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Home from "./components/pages/Home";
import Login from "./components/pages/Login";
import Signup from "./components/pages/Signup";
import About from "./components/pages/About";
import Layout from "./Layout";

const App = () => {
    return (<Router>
        <Layout>
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/login" element={<Login />} />
                <Route path="/signup" element={<Signup />} />
                <Route path="/about" element={<About />} />
            </Routes>
        </Layout>
    </Router>);
}

export default App;
