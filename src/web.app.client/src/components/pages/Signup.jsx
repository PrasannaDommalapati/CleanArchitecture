import { useEffect, useState } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faClose, faAddressCard } from "@fortawesome/free-solid-svg-icons";
import { PostRegisterUser } from "../../api/index";

const Signup = () => {
  const [isError, setIsError] = useState(false);
  const [isLoading, setIsLoading] = useState(false);
  const [formData, setFormData] = useState({
    email: "",
    password: "",
    confirmPassword: "",
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

    if (formData.password != formData.confirmPassword) {
      setIsError(true);
    }

    var response = await PostRegisterUser(formData);
  };

  return (
    <div className="signup__container">
      <form onSubmit={onHandleSubmit} className="form__signup">
        {isError && (
          <div className="errorMessage text-danger">
            Failed to register the user
          </div>
        )}
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
          <label htmlFor="username" className="col-sm-2 col-form-label">
            Username
          </label>
          <input
            type="username"
            className="form-control"
            id="username"
            placeholder="Enter Username"
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
        <div className="form-group row">
          <label htmlFor="confirmPassword">Confirm Password</label>
          <input
            type="password"
            className="form-control"
            id="confirmPassword"
            placeholder="Confirm Password"
            onChange={onHandleChange}
          />
        </div>
        <div className="form-group row">
          <p>Already have an account? <a className="has-account" href="/login">
            Login
          </a></p>
          
        </div>

        <div className="form__buttons">
          <button type="button" className="btn btn-secondary">
            Cancel
          </button>
          <button type="submit" className="btn btn-primary">
            Sign Up
          </button>
        </div>
      </form>
    </div>
  );
};

export default Signup;
