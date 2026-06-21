import { useState } from 'react'
import { BrowserRouter, Routes, Route, Link } from "react-router";
import ProdPage from './Pages/ProdPage';
import Home from './Pages/Home'
import './App.css'

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/Product/:id" element={<ProdPage />} /></Routes>
    </BrowserRouter>
  )
}

export default App
