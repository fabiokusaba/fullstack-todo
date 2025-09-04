import { useEffect, useState } from 'react'
import { todoClient } from './baseUrl'
import type { CreateTodoDto, Todo } from './generated-ts-client'

function App() {
  const [todos, setTodos] = useState<Todo[]>([])
  const [myForm, setMyForm] = useState<CreateTodoDto>({
    title: '',
    description: '',
    priority: 0
  })

  useEffect(() => {
    todoClient.getAllTodos().then(r => {
      setTodos(r)
    })
  }, [])

  return (
    <>
    <input 
      value={myForm.title} 
      onChange={e => setMyForm({...myForm, title: e.target.value})} 
      placeholder='your interesting title' 
    />

    <input 
      value={myForm.description} 
      onChange={e => setMyForm({...myForm, description: e.target.value})} 
      placeholder='your interesting description' 
    />

    <input 
      value={myForm.priority} 
      onChange={e => setMyForm({...myForm, priority: Number.parseInt(e.target.value)})} 
      type='number' 
      placeholder='your interesting priority' 
    />

    <button onClick={() => {
      todoClient.createTodo(myForm).then(result => {
        console.log('todo was created successfully')
        
        setTodos([...todos, result])

        setMyForm({
          title: '',
          description: '',
          priority: 0
        })
      })
    }}>Create a new todo</button>

    < hr />

    {
      todos.map(t => {
        return <div key={t.id}>
          {JSON.stringify(t)}
        </div>
      })
    }
    </>
  )
}

export default App
