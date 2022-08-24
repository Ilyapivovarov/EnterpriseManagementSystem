import React from 'react';
import {InputLabel, TextField, Typography} from '@mui/material';

interface EditableTextFieldProps {
  id: string,
  variant: 'standard' | 'outlined',
  onChange?: (value? : string) => void
  lable?: string,
  multiline: boolean,
  fullWidth: boolean,
  isEditable: boolean,
  placeholder?: string,
  value?: string,
}

const EditableTextField: React.FC<EditableTextFieldProps> = ({isEditable,
  value, id, lable,
  fullWidth, multiline, variant, placeholder, onChange}) => {
  const [newValue, setNewValue] = React.useState<string>(value ? value : '');

  React.useEffect(() => {
    setNewValue(value ? value : '');
  }, [isEditable]);

  const onChangeHandler = (e : React.ChangeEvent<HTMLTextAreaElement | HTMLInputElement>) => {
    e.preventDefault();
    const value = e.target.value;
    setNewValue(value);
    if (onChange) {
      onChange(value);
    }
  };

  return (
    <>
      {isEditable ?
        <TextField
          style={{marginLeft: '10px'}}
          id={id}
          label={lable}
          onChange={onChangeHandler}
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
