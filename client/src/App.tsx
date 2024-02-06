import { Box } from '@mui/material';
import React from 'react';
import { Route, RouterProvider, Routes, createBrowserRouter, redirect } from 'react-router-dom';
import Tasks from './Components/Tasks/Tasks';
import Login from './Components/Login/Login';
import Header from './Components/Header/Header';
import Home from './Components/Home/Home';
import { IsLoggedIn } from './API/AuthAPI';

const router = createBrowserRouter([
  {
    element: <Header />,
    children: [
      {
        path: "/",
        element: <Home />
      },
      {
        path: "/Tasks",
        element: <Tasks />,
        loader: async () => requireAuth()
      },
      {
        path: "/Login",
        element: <Login />,
      }
    ]
  }
])

function App() {
  return (
    <RouterProvider router={router} />
  );
}

function requireAuth() {
  if(!IsLoggedIn()){
    return redirect("/Login");
  }

  return null;
}

export default App;
