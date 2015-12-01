using ARPGCommon.Model;
using System.Collections.Generic;


public delegate void OnGetRoleEvent(List<Role> roles);

public delegate void OnAddRoleEvent(Role role);

public delegate void OnSelectRoleEvent();

public delegate void OnUpdateRoleEvent();

public delegate void OnGetTaskDbEvent(List<TaskDb> taskDbs);

public delegate void OnAddTaskDbEvent(TaskDb taskDb);

public delegate void OnUpdateTaskDbEvent(TaskDb taskDb);

public delegate void OnGetServerListEvent(List<ServerProperty> serverProperties);