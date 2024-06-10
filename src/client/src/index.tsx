import '@fontsource/roboto/300.css';
import '@fontsource/roboto/400.css';
import '@fontsource/roboto/500.css';
import '@fontsource/roboto/700.css';
import { PaletteMode } from '@mui/material';
import { blue, grey } from '@mui/material/colors';
import ReactDOM from 'react-dom/client';
import App from './App';
import './index.css';

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);

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

root.render(
    <App />
);
