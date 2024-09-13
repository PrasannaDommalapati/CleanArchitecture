import axios from "axios";

export const PostRegisterUser = async body => {
    await axios.post("/api/register", {
        email: body.email,
        password: body.password
    })
};
export const PostLogin = async body => {
    await axios.post("/api/login", {
        email: body.email,
        password: body.password
    })
};