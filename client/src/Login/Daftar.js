import { useRef, useState, useEffect } from "react";
import { faCheck, faTimes, faInfoCircle } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useLocation, useNavigate } from "react-router-dom";
import axios from '../api/axios';
import {Link} from 'react-router-dom';
import Login from "./Masuk";

const USER_REGEX = /^(?=.*[a-z]).{5,23}$/;
const PWD_REGEX = /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9]).{8,24}$/;
const NAMA_REGEX = /^(?=.*[a-z])(?=.*[A-Z]).{8,23}$/;
const EMAIL_REGEX = /^(?=.*[@]).{11,50}$/;
const TELEPON_REGEX = /^(?=.*[0-9]).{11,16}$/;
const REGISTER_URL = '/api/TblUser/Daftar';

const Register = () => {
    const navigate = useNavigate();
    const location = useLocation();
    const from = location.state?.from?.pathname || "/Daftar";
    const userRef = useRef();
    const errRef = useRef();

    const [user, setUser] = useState('');
    const [validUser, setValidUser] = useState(false);
    const [userFocus, setUserFocus] = useState(false);

    const [pwd, setPwd] = useState('');
    const [validPwd, setValidPwd] = useState(false);
    const [pwdFocus, setPwdFocus] = useState(false);

    const [nama, setNama] = useState('');
    const [validNama, setValidNama] = useState(false);
    const [namaFocus, setNamaFocus] = useState(false);

    const [email, setEmail] = useState('');
    const [validEmail, setValidEmail] = useState(false);
    const [emailFocus, setEmailFocus] = useState(false);

    const [telepon, setTelepon] = useState('');
    const [validTelepon, setValidTelepon] = useState(false);
    const [teleponFocus, setTeleponFocus] = useState(false);

    const [matchPwd, setMatchPwd] = useState('');
    const [validMatch, setValidMatch] = useState(false);
    const [matchFocus, setMatchFocus] = useState(false);

    const [errMsg, setErrMsg] = useState('');
    const [success, setSuccess] = useState(false);

    const [roles, setRoles] = useState('');

    let match;

    useEffect(() => {
        userRef.current.focus();
    }, [])

    useEffect(() => {
        setValidUser(USER_REGEX.test(user));
    }, [user])

    useEffect(() => {
        setValidNama(NAMA_REGEX.test(nama))
    }, [nama])

    useEffect(() => {
        setValidEmail(EMAIL_REGEX.test(email));
    }, [email])

    useEffect(() => {
        setValidTelepon(TELEPON_REGEX.test(telepon))
    }, [telepon])

    useEffect(() => {
        setValidPwd(PWD_REGEX.test(pwd));
        setValidMatch(pwd === matchPwd);
    }, [pwd, matchPwd])

    useEffect(() => {
        setErrMsg('');
    }, [user, nama, ,email, telepon, pwd, matchPwd])

    const handleSubmit = async (e) => {
        e.preventDefault();
        // if button enabled with JS hack
        const v1 = USER_REGEX.test(user);
        const v2 = PWD_REGEX.test(pwd);
        const v3 = NAMA_REGEX.test(nama);
        const v4 = EMAIL_REGEX.test(email);
        const v5 = TELEPON_REGEX.test(telepon);
        if (!v1 || !v2 || !v3 ||!v4 ||!v5) {
            setErrMsg("Invalid Entry");
            return;
        }
        try {
            const response = await axios.post(REGISTER_URL,
                JSON.stringify({ Tipe:tipe, Username:user, Password:pwd, Nama:nama, Email:email, Telepon:telepon }),
                {
                    headers: { 'Content-Type': 'application/json' },
                    withCredentials: true
                }
            );
            console.log(response?.data);
            console.log(response?.accessToken);
            console.log(JSON.stringify(response))
            setSuccess(true);
            //clear state and controlled inputs
            //need value attrib on inputs for this
            setNama('');
            setUser('');
            setPwd('');
            setEmail('');
            setTelepon('');
            setMatchPwd('');
        } catch (err) {
            if (!err?.response) {
                setErrMsg('No Server Response');
            } else if (err.response?.status === 409) {
                setErrMsg('Username Taken');
            } else {
                setErrMsg('Registration Failed')
            }
            errRef.current.focus();
        }
    }

    return (
        <>
            {success ? (
                <section>
                    <h1>Success!</h1>
                    <br/>
                </section>
            ) : (
                <section >
                    <p ref={errRef} className={errMsg ? "errmsg" : "offscreen"} aria-live="assertive">{errMsg}</p>
                    <h1>Register</h1>
                    <form onSubmit={handleSubmit}>
                        <label htmlFor="nama">
                            Nama:
                            <FontAwesomeIcon icon={faCheck} className={validNama ? "valid" : "hide"} />
                            <FontAwesomeIcon icon={faTimes} className={validNama || !nama ? "hide" : "invalid"} />
                        </label>
                        <input
                            type="text"
                            id="nama"
                            ref={userRef}
                            autoComplete="off"
                            onChange={(e) => setNama(e.target.value)}
                            value={nama}
                            required
                            aria-invalid={validNama ? "false" : "true"}
                            aria-describedby="uidnama"
                            onFocus={() => setNamaFocus(true)}
                            onBlur={() => setNamaFocus(false)}
                        />
                        <p id="uidnama" className={namaFocus && !validNama ? "instructions" : "offscreen"}>
                            <FontAwesomeIcon icon={faInfoCircle} />
                            <br />
                            Harus Menggunakan Minimal 1 Huruf Besar<br />
                            Minimal 7 Karakter <br />
                        </p>

                        <label htmlFor="username">
                            Username:
                            <FontAwesomeIcon icon={faCheck} className={validUser ? "valid" : "hide"} />
                            <FontAwesomeIcon icon={faTimes} className={validUser || !user ? "hide" : "invalid"} />
                        </label>
                        <input
                            type="text"
                            id="user"
                            ref={userRef}
                            autoComplete="off"
                            onChange={(e) => setUser(e.target.value)}
                            value={user}
                            required
                            aria-invalid={validUser ? "false" : "true"}
                            aria-describedby="uidnote"
                            onFocus={() => setUserFocus(true)}
                            onBlur={() => setUserFocus(false)}
                        />
                        <p id="uidnote" className={userFocus && !validUser ? "instructions" : "offscreen"}>
                            <FontAwesomeIcon icon={faInfoCircle} />
                            characters.<br />
                            Must begin with a letter.<br />
                            Letters, numbers, underscores, hyphens allowed.
                        </p>

                        <label htmlFor="email">
                            Email:
                            <FontAwesomeIcon icon={faCheck} className={validEmail ? "valid" : "hide"} />
                            <FontAwesomeIcon icon={faTimes} className={validEmail || !email ? "hide" : "invalid"} />
                        </label>
                        <input
                            type="text"
                            id="email"
                            ref={userRef}
                            autoComplete="off"
                            onChange={(e) => setEmail(e.target.value)}
                            value={email}
                            required
                            aria-invalid={validEmail ? "false" : "true"}
                            aria-describedby="emailnote"
                            onFocus={() => setEmailFocus(true)}
                            onBlur={() => setEmailFocus(false)}
                        />
                        <p id="emailnote" className={emailFocus && !validEmail ? "instructions" : "offscreen"}>
                            <FontAwesomeIcon icon={faInfoCircle} />
                            8 to 24 characters.<br />
                            Must include uppercase and lowercase letters, a number and a special character.<br />
                            Allowed special characters: <span aria-label="exclamation mark">!</span> <span aria-label="at symbol">@</span> <span aria-label="hashtag">#</span> <span aria-label="dollar sign">$</span> <span aria-label="percent">%</span>
                        </p>

                        <label htmlFor="telepon">
                            Telepon:
                            <FontAwesomeIcon icon={faCheck} className={validTelepon ? "valid" : "hide"} />
                            <FontAwesomeIcon icon={faTimes} className={validTelepon || !telepon ? "hide" : "invalid"} />
                        </label>
                        <input
                            type="text"
                            id="telepon"
                            ref={userRef}
                            autoComplete="off"
                            onChange={(e) => setTelepon(e.target.value)}
                            value={telepon}
                            required
                            aria-invalid={validTelepon ? "false" : "true"}
                            aria-describedby="teleponnote"
                            onFocus={() => setTeleponFocus(true)}
                            onBlur={() => setTeleponFocus(false)}
                        />
                        <p id="teleponnote" className={teleponFocus && !validTelepon ? "instructions" : "offscreen"}>
                            <FontAwesomeIcon icon={faInfoCircle} />
                            8 to 24 characters.<br />
                            Must include uppercase and lowercase letters, a number and a special character.<br />
                            Allowed special characters: <span aria-label="exclamation mark">!</span> <span aria-label="at symbol">@</span> <span aria-label="hashtag">#</span> <span aria-label="dollar sign">$</span> <span aria-label="percent">%</span>
                        </p>

                        <label htmlFor="password">
                            Password:
                            <FontAwesomeIcon icon={faCheck} className={validPwd ? "valid" : "hide"} />
                            <FontAwesomeIcon icon={faTimes} className={validPwd || !pwd ? "hide" : "invalid"} />
                        </label>
                        <input
                            type="password"
                            id="password"
                            onChange={(e) => setPwd(e.target.value)}
                            value={pwd}
                            required
                            aria-invalid={validPwd ? "false" : "true"}
                            aria-describedby="pwdnote"
                            onFocus={() => setPwdFocus(true)}
                            onBlur={() => setPwdFocus(false)}
                        />
                        <p id="pwdnote" className={pwdFocus && !validPwd ? "instructions" : "offscreen"}>
                            <FontAwesomeIcon icon={faInfoCircle} />
                            8 to 24 characters.<br />
                            Must include uppercase and lowercase letters, a number and a special character.<br />
                            Allowed special characters: <span aria-label="exclamation mark">!</span> <span aria-label="at symbol">@</span> <span aria-label="hashtag">#</span> <span aria-label="dollar sign">$</span> <span aria-label="percent">%</span>
                        </p>


                        <label htmlFor="confirm_pwd">
                            Confirm Password:
                            <FontAwesomeIcon icon={faCheck} className={validMatch && matchPwd ? "valid" : "hide"} />
                            <FontAwesomeIcon icon={faTimes} className={validMatch || !matchPwd ? "hide" : "invalid"} />
                        </label>
                        <input
                            type="password"
                            id="confirm_pwd"
                            onChange={(e) => setMatchPwd(e.target.value)}
                            value={matchPwd}
                            required
                            aria-invalid={validMatch ? "false" : "true"}
                            aria-describedby="confirmnote"
                            onFocus={() => setMatchFocus(true)}
                            onBlur={() => setMatchFocus(false)}
                        />
                        <p id="confirmnote" className={matchFocus && !validMatch ? "instructions" : "offscreen"}>
                            <FontAwesomeIcon icon={faInfoCircle} />
                            Must match the first password input field.
                        </p>
                        <label for="tipe"/>
                            <select id="tipe" name="tipe">
                            <option value="volvo">user</option>
                            {/* <option value="saab">Saab</option> */}
                            </select>

                        <button disabled={!validNama || !validUser || !validEmail || !validPwd || !validTelepon || !validMatch ? true : false}>Sign Up</button>
                    </form>
                    <p>
                        Already registered?<br />
                        <span className="line">
                            <button>Sign In</button>
                            <Link to={Login} /> 
                        </span>
                    </p>
                </section>
            )}
        </>
    )
}

export default Register