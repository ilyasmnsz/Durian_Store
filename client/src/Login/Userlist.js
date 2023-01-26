// import React, { useState, useEffect } from 'react'
// import './App.css';
// import axios from "../api/axios";

// function App() {
//   const url ='/api/TblUser/List';

//   const [users, setUsers] = useState([])

//   const getDataUsers = async () => {
//     const response = await axios.get(url, JSON.stringify({ Nama, Username, Password }));
//     const dataku = await response.JSON.stringify(response.data)
//     const users = dataku.slice(0, 8)
//     setUsers(users)
//   }

//   useEffect(() => {
//     getDataUsers()
//   }, [])

//   return (
//     <div className="App">
//       <h3>List Users</h3>
//       {users.map((user) => {
//         return (
//           <p key={user.id}>
//             {user.login}
//           </p>
//         )
//       })}
//     </div>
//   );
// }

// export default App;