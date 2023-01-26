import { useContext } from "react";
import AuthContext from "../context/AuthProvider";

function UseAuth() {
    return useContext(AuthContext);
}

export default UseAuth;