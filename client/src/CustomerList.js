// import React, { useState, useEffect} from "react";
// import axios from "./api/axios";

// const DURIAN_URL = '/api/TblUser/List';

// const DurianList = () => {
//     const [durian, setDurian] = useState([]);

//     useEffect(() => {
//         getDurian();
//     },[]);

//     const getDurian = async () => {
//         const response = await axios.get(DURIAN_URL);
//         setDurian(response.data.DURIAN_URL);
//     };

//     return (
//         <div className="column mt-5 is centered">
//             <div className="column is half">
//                 <table className="table is-stripped is fullwidth">
//                     <thead>
//                         <tr>
//                             <th>No</th>
//                             <th>Durian</th>
//                             <th>Action</th>
//                         </tr>
//                     </thead>
//                     <tbody>
//                         {durian.map((durian, index) => (
//                             <tr key={durian.id}>
//                                 <td>{index ++}</td>
//                                 <td>{durian.Namadurian}</td>
//                             </tr>
//                         ))}
//                     </tbody>
//                 </table>
//             </div>
//         </div>
//     );
// }

// export default DurianList;