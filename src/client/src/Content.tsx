import { RouterProvider, createBrowserRouter, redirect } from 'react-router-dom';
import Header from './Components/Header/Header';
import Home from './Components/Home/Home';
import Login from './Components/Login/Login';
import Tasks from './Components/Tasks/Tasks';
import MyWorkspacesPage from './Components/Workspaces/MyWorkspaces/MyWorkspacesPage';
import { IsLoggedIn } from './Helpers/LoggedInHelper';
import AccountPage from './Components/Account/AccountPage';
import WorkspacePage from './Components/Workspaces/Workspace/WorkspacesPage';

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
          path: "/workspaces",
          element: <MyWorkspacesPage />,
          loader: async () => requireAuth() 
        },
        {
          path: "/workspaces/:workspaceId",
          element: <WorkspacePage />
        },
        {
          path: "/account",
          element: <AccountPage />,
          loader: async () => requireAuth()
        },
        {
          path: "/tasks",
          element: <Tasks />,
        },
        {
          path: "/login",
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