import React from 'react';
import {TextField, Typography} from '@mui/material';

interface EditableTextFieldProps {
  id: string,
  variant: 'standard' | 'outlined'
  lable?: string,
  multiline: boolean,
  fullWidth: boolean,
  isEditable: boolean,
  value?: string | null,
}

const EditableTextField: React.FC<EditableTextFieldProps> = ({isEditable, value, id, lable, fullWidth, multiline}) => {
  const [newValue, setNewValue] = React.useState<string>(value ? value : '');

  const onChange = (e : React.ChangeEvent<HTMLTextAreaElement | HTMLInputElement>) => {
    e.preventDefault();
    setNewValue(e.target.value);
  };

  if (isEditable) {
    return <TextField
      id={id}
      label={lable}
      variant="outlined"
      value={newValue}
      onChange={onChange}
      fullWidth={fullWidth}
      multiline={multiline}
      rows={10}/>;
  } else {
    return (<div>
      <Typography
        id={id}
        paddingBottom={2}
        paddingTop={1}
        variant="h3"
        paddingLeft={1}
      >
        {value}
      </Typography>
    </div>);
  }
};

export default EditableTextField;
