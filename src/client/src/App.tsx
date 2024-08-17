import { CssBaseline, ThemeProvider, createTheme, useMediaQuery } from '@mui/material';
import { blue, grey } from '@mui/material/colors';
import React from 'react';
import { Provider } from 'react-redux';
import Content from './Content';
import { store } from './store';
import { SnackbarProvider } from 'notistack';
;

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
    <SnackbarProvider>
      <Provider store={store}>
        <ThemeProvider theme={theme}>
          <CssBaseline />
          <Content />
        </ThemeProvider>
      </Provider>
    </SnackbarProvider>
  );
}

export default App;