import React from 'react';
import {InputLabel, TextField, Typography} from '@mui/material';

interface EditableTextFieldProps {
    id: string,
    required?: boolean,
    error?: boolean,
    errorMessage?: string | undefined,
    variant: 'standard' | 'outlined',
    onChange?: (value?: string) => void,
    lable?: string,
    multiline: boolean,
    fullWidth: boolean,
    isEditable: boolean,
    placeholder?: string,
    value?: string,
}

const EditableTextField: React.FC<EditableTextFieldProps> = ({
  isEditable,
  value, id, lable,
  fullWidth, multiline, variant, placeholder, onChange,
  required, error, errorMessage,
}) => {
  React.useEffect(() => {

  }, [isEditable]);

  const onChangeHandler = (e: React.ChangeEvent<HTMLTextAreaElement | HTMLInputElement>) => {
    e.preventDefault();
    const value = e.target.value;
    if (onChange) {
      onChange(value);
    }
  };

  return (
    <>
      {isEditable ?
                <TextField
                  required={required}
                  error={error}
                  style={{marginLeft: '10px'}}
                  id={id}
                  label={lable}
                  onChange={onChangeHandler}
                  multiline={multiline}
                  defaultValue={value}
                  minRows={5}
                  fullWidth={fullWidth}
                  variant={variant}
                  helperText={errorMessage}
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
