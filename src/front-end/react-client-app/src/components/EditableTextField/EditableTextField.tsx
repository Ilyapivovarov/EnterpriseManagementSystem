import React from 'react';
import EditIcon from '@mui/icons-material/Edit';
import {Box, TextField, Tooltip} from '@mui/material';
import IconButton from '@mui/material/IconButton';
import SaveIcon from '@mui/icons-material/Save';

interface EditableTextFieldProps {
  id: string,
  variant: 'standard' | 'outlined'
  lable?: string,
  multiline: boolean,
  fullWidth: boolean,
  isEditable: boolean,
  value?: string | null,
}

const EditableTextField: React.FC<EditableTextFieldProps> = ({isEditable,
  value, id, lable,
  fullWidth, multiline, variant}) => {
  const [isEditMode, setIsEditMode] = React.useState(false);
  const [newValue, setNewValue] = React.useState<string>(value ? value : '');

  const onChange = (e : React.ChangeEvent<HTMLTextAreaElement | HTMLInputElement>) => {
    e.preventDefault();
    setNewValue(e.target.value);
  };

  return (
    <Box>
      {
          isEditMode ?
            <Tooltip title={`Edit ${lable?.toLowerCase()}`}>
              <IconButton onClick={() => setIsEditMode(false)}>
                <SaveIcon />
              </IconButton>
            </Tooltip> :
           <Tooltip title={`Edit ${lable?.toLowerCase()}`}>
             <IconButton onClick={() => setIsEditMode(true)}>
               <EditIcon />
             </IconButton>
           </Tooltip>

      }
      <TextField
        style={{marginLeft: '10px'}}
        id={id}
        label={lable}
        onChange={onChange}
        value={newValue}
        fullWidth={fullWidth}
        variant={isEditMode ? 'outlined' : 'standard'}
        InputProps={{
          readOnly: !isEditMode,
        }}
      />
    </Box>
  );
};

export default EditableTextField;
