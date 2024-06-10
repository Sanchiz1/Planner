import { Box, CssBaseline, PaletteMode, ThemeProvider, createTheme, useMediaQuery } from '@mui/material';
import React from 'react';
import { Route, RouterProvider, Routes, createBrowserRouter, redirect } from 'react-router-dom';
import Tasks from './Components/Tasks/Tasks';
import Login from './Components/Login/Login';
import Header from './Components/Header/Header';
import Home from './Components/Home/Home';
import { IsLoggedIn } from './API/AuthAPI';
import Workspaces from './Components/Workspaces/Workspaces';
import { blue, grey } from '@mui/material/colors';
import { useTheme } from '@emotion/react';

const Theme = createTheme({
  palette: {
    mode: 'dark',
    primary: {
      main: grey[900]
    },
    secondary: {
      main: blue[800]
    }
  },
});

const getDesignTokens = (mode: PaletteMode) => ({
  palette: {
    mode,
    ...(mode === 'light'
      ? {
        primary: {
          main: grey[900]
        },
        secondary: {
          main: blue[700]
        }
      }
      : {
        primary: {
          main: grey[900]
        },
        secondary: {
          main: blue[700]
        }
      }),
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

function App() {
  const prefersDarkMode = useMediaQuery('(prefers-color-scheme: dark)');

  const theme = React.useMemo(
    () =>
      createTheme({
        palette:
          prefersDarkMode ?
            {
              mode: 'dark',
              primary: {
                main: grey[300]
              },
              secondary: {
                main: blue[800]
              }
            }
            :
            {
              mode: 'light',
              primary: {
                main: grey[900]
              },
              secondary: {
                main: blue[800]
              }
            },
      }),
    [prefersDarkMode],
  );

  return (
    <ThemeProvider theme={theme}>
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