import Daftar from "./Login/Daftar";  
import Masuk from "./Login/Masuk";
import { Routes, Route } from "react-router-dom";
import "./index.css"
import Layout from "./Layout";
import RequireAuth from "./Login/RequireAuth";
import Beranda from "./Beranda";
import TakTerdaftar from "./Login/TakTerdaftar";
import LinkPage from "./LinkPage";
import CustomerList from "./CustomerList";

function App() {

  return (
    <Routes>
      {/* <Route path="/" element={< Layout/>}/> */}
        {/* public routes */}
        <Route path="/" element={<Masuk />} />   
        <Route path="Daftar" element={<Daftar />} />
        <Route path="TakTerdaftar" element={< TakTerdaftar />} />
        {/* <Route path="customer" element={<CustomerList />} /> */}
        <Route path="link" element={<LinkPage />} />   
      <Route element={<RequireAuth allowedRoles={"admin"}/>}>  
        <Route path="Beranda" element={<Beranda />} />
      </Route> 
    </Routes>
  );
}

export default App;
