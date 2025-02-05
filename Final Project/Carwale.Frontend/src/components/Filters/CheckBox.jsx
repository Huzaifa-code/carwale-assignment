import React from 'react';

const Checkbox = ({ id, name, checked, label,onChange }) => {
  return (
    <div style={{ marginBottom: '10px' }}>
      <input 
        type="checkbox" 
        id={id} 
        name={name} 
        checked={checked} 
        onChange={onChange} 
      />
      <label htmlFor={id} style={{fontSize: '0.8rem'}} >{label}</label>
    </div>
  );
};

export default Checkbox;
