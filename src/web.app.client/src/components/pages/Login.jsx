import { useState } from "react";
import {PostLogin} from "../../api/index";
const Login = () => {

    const [isError, setIsError] = useState(false);
    const [isLoading, setIsLoading] = useState(false);
    const [formData, setFormData] = useState({
        email: "",
        password: ""
    });
    const onHandleChange = (event) => {
        setFormData((prev) => ({
            ...prev,
            [event.target.id]: event.target.value,
        }));
    };

    const onHandleSubmit = async (event) => {
        event.preventDefault();
        setIsLoading(true);

        var response = await PostLogin(formData);
        console.log(response);
    };

    return (<div className="login__container">
        <form onSubmit={onHandleSubmit} className="form__login">
            {isError && <div className="errorMessage text-danger">Failed to register the user</div>}
            <div className="form-group row">
                <label htmlFor="email" className="col-sm-2 col-form-label">
                    Email
                </label>
                <input
                    type="email"
                    className="form-control"
                    id="email"
                    placeholder="Enter email"
                    onChange={onHandleChange}
                />
            </div>
          
            <div className="form-group row">
                <label htmlFor="password">Password</label>
                <input
                    type="password"
                    className="form-control"
                    id="password"
                    placeholder="Password"
                    onChange={onHandleChange}
                />
            </div>
            <div className="form-check">
                <span>
                    <input type="checkbox" className="form-check-input" id="remember" />
                    <label className="form-check-label" htmlFor="remember">
                        Remember password
                    </label>
                </span>
                <span>
                    Not Registered?
                    <a className="has-account" href="/signup">
                        Signup
                    </a>
                </span>
            </div>

            <div className="form__buttons">
                <button type="button" className="btn btn-secondary">
                    Cancel
                </button>
                <button type="submit" className="btn btn-primary">
                    Login
                </button>
            </div>
        </form>
    </div>
  );
};

export default Login;