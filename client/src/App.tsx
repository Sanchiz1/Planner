import { Box } from '@mui/material';
import React from 'react';
import { Route, Routes } from 'react-router-dom';
import Tasks from './Components/Tasks/Tasks';
import Login from './Components/Login/Login';

function App() {
  return (
    <Box
        sx={{
          height: 'calc(100vh)'
        }}>
          <Routes>
            <Route path='/' element={<Tasks />}/>
            <Route path='/Login' element={<Login />}/>
          </Routes>
    </Box>
  );
}

export default App;
