import { RouterProvider, createBrowserRouter, redirect } from 'react-router-dom';
import Header from './Components/Header/Header';
import Home from './Components/Home/Home';
import Login from './Components/Login/Login';
import Tasks from './Components/Tasks/Tasks';
import Workspaces from './Components/Workspaces/Workspaces';
import { IsLoggedIn } from './Helpers/LoggedInHelper';
import AccountPage from './Components/Account/AccountPage';

function Content() {
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
          loader: async () => requireAuth() 
        },
        {
          path: "/Account",
          element: <AccountPage />,
          loader: async () => requireAuth()
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
  ]);

  return (
    <RouterProvider router={router} />
  );
}

function requireAuth() {
  if (!IsLoggedIn()) {
    return redirect("/Login");
  }

  return null;
}

export default Content;