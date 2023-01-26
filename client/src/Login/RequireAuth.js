import { useEffect } from "react";
import { useLocation, Navigate, Outlet } from "react-router-dom";
import UseAuth from "../Hooks/UseAuth";

const RequireAuth = ({ allowedRoles }) => {
    const { auth } = UseAuth();
    const location = useLocation();

    useEffect(() => {
        console.info(auth.tipe);
        console.info(allowedRoles);
    }, [auth])

    return(
        (auth.tipe === allowedRoles)
            ? <Outlet />
            : auth?.user
                ? <Navigate to="/TakTerdaftar" state={{ from: location }} replace />
                : <Navigate to="/" state={{ from: location }} replace />
    );
}

export default RequireAuth;