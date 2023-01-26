import { Link } from "react-router-dom"

const LinkPage = () => {
    return (
        <section>
            <h1>Links</h1>
            <br />
            <h2>Public</h2>
            <Link to="/">Login</Link>
            <Link to="/Daftar">Register</Link>
            <br />
            <h2>Private</h2>
            <Link to="/Beranda">Home</Link>
            <Link to="/edit">Editors Page</Link>
            <Link to="/admin">Admin Page</Link>
        </section>
    )
}

export default LinkPage