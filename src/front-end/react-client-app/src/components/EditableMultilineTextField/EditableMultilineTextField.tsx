import React from 'react';
import {Box, Button, ButtonGroup, InputLabel, TextField, Tooltip} from '@mui/material';
import SaveIcon from '@mui/icons-material/Save';
import EditIcon from '@mui/icons-material/Edit';
import IconButton from '@mui/material/IconButton';

interface EditableMultilineTextFieldProps {
  id: string,
  placeholder?: string,
  lable?: string,
  fullWidth: boolean,
  isEditable: boolean,
  value?: string | null,
  onSaveHandler: (value: string) => void
}

const EditableMultilineTextField: React.FC<EditableMultilineTextFieldProps> = ({isEditable,
  value, id, lable,
  fullWidth, placeholder, onSaveHandler}) => {
  const [isEditMode, setIsEditMode] = React.useState(false);
  const [newValue, setNewValue] = React.useState<string>(value ? value : '');

  const onChange = (e : React.ChangeEvent<HTMLTextAreaElement | HTMLInputElement>) => {
    e.preventDefault();
    setNewValue(e.target.value);
  };

  const onSave = () => {
    setIsEditMode(false);
    onSaveHandler(newValue);
  };

  return (
    <Box>
      <Box style={{padding: '5px', color: 'gray'}} display={'flex'} justifyContent={'space-between'}>
        <InputLabel children={lable}/>
        <ButtonGroup size={'small'}>
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
        </ButtonGroup>
      </Box>
      <TextField
        id={id}
        onChange={onChange}
        value={newValue}
        fullWidth={fullWidth}
        multiline={true}
        minRows={5}
        placeholder={placeholder}
        variant={'outlined'}
        InputProps={{
          readOnly: !isEditMode,
        }}
      />
    </Box>

  );
};

export default EditableMultilineTextField;
