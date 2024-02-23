import { Box, CssBaseline, PaletteMode, ThemeProvider, createTheme } from '@mui/material';
import React from 'react';
import { Route, RouterProvider, Routes, createBrowserRouter, redirect } from 'react-router-dom';
import Tasks from './Components/Tasks/Tasks';
import Login from './Components/Login/Login';
import Header from './Components/Header/Header';
import Home from './Components/Home/Home';
import { IsLoggedIn } from './API/AuthAPI';
import Workspaces from './Components/Workspaces/Workspaces';
import { blue, grey } from '@mui/material/colors';

const Theme = createTheme({
  palette: {
    primary: {
      main: grey[900]
    },
    secondary: {
      main: blue[800]
    }
  },
});

const router = createBrowserRouter([
  {
    element: <Header />,
    children: [
      {
        path: "/",
        element: <Home />
      },
      {
        path: "/Workspaces",
        element: <Workspaces />,
        //loader: async () => requireAuth()
      },
      {
        path: "/Tasks",
        element: <Tasks />,
      },
      {
        path: "/Login",
        element: <Login />,
      }
    ]
  }
])

const ColorModeContext = React.createContext({ toggleColorMode: () => { } });

function App() {

  return (
    <ThemeProvider theme={Theme}>
      <CssBaseline />
      <RouterProvider router={router} />
    </ThemeProvider>
  );
}

function requireAuth() {
  if (!IsLoggedIn()) {
    return redirect("/Login");
  }

  return null;
}

export default App;