import { useNavigate } from "react-router-dom";

const TakTerdaftar = () => {
    const navigate = useNavigate();
    const kembali = () =>  navigate(-1);

    return (
        <section>
            <h1>Tak Terdaftar</h1>
            <br />
            <p>Anda Tidak Mempunyai Akses Di Halaman Ini.</p>
            <div className="flexGrow">
                <button onClick={kembali}> Kembali</button>
            </div>
        </section>
    )
}

export default TakTerdaftar;