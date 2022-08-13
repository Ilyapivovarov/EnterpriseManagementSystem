import React from 'react';
import {InputLabel, TextField, Typography} from '@mui/material';

interface EditableTextFieldProps {
  id: string,
  variant: 'standard' | 'outlined'
  lable?: string,
  multiline: boolean,
  fullWidth: boolean,
  isEditable: boolean,
  placeholder?: string,
  value?: string,
}

const EditableTextField: React.FC<EditableTextFieldProps> = ({isEditable,
  value, id, lable,
  fullWidth, multiline, variant, placeholder}) => {
  const [newValue, setNewValue] = React.useState<string>(value ? value : '');

  React.useEffect(() => {
    setNewValue(value ? value : '');
  }, [isEditable]);

  const onChange = (e : React.ChangeEvent<HTMLTextAreaElement | HTMLInputElement>) => {
    e.preventDefault();
    setNewValue(e.target.value);
  };

  return (
    <>
      {isEditable ?
        <TextField
          style={{marginLeft: '10px'}}
          id={id}
          label={lable}
          onChange={onChange}
          multiline={multiline}
          value={newValue}
          minRows={5}
          fullWidth={fullWidth}
          variant={variant}
          InputProps={{
            readOnly: !isEditable,
          }}
        /> :
        <>
          <InputLabel>{lable}</InputLabel>
          <Typography>
            {value ? value : placeholder}
          </Typography>
        </>
      }

    </>
  );
};

export default EditableTextField;
