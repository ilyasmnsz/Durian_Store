import { Outlet } from "react-router-dom";
import "./index.css"

const Layout = () => {
    return (
        <main className="App">
            <Outlet/>
            <h1>Layout</h1>
        </main>
    )
}

export default Layout