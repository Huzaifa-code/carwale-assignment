## GIT Assignment

What i have learnt in git

Git is version control system , And github is a hosting service for hosting code.

There are various steps on pushing the code to a github  repository .

Github repository is a remote space on server we put our code .


## Create a repo 

To create a repo on github got to [github.com](github.com) create account there you will find there in home to create a new repository

![](https://i.imgur.com/qoNSBvR.png)

There are  2 types of repo 'Public' and 'Private'.

'Public' repos anyone can see your code.

'Private' repo only people whom you give access can see it.

## Steps :

1. Initialize a repo
2. Add
3. Commit 
4. Push


### 1. Initializing git repo

Initializing repo locally :

```
git init
```

Then enter details it asks in terminal like author , licence, version , github repo link...

Connect local repository created with github repo that you created on [github.com](github.com)

```
git remote add origin <repo-link> 
```


### 2. Commands for Add :

```
git add <filenames>
```

Instead of filename we can also use '.' or '-p'

This adds all files to staging area
```
git add .
```


By '-p' it will show in terminal the changes you have made in code per file and you need to enter 'y' for yes, 'n' for no, 'd' to discard all changes in this file for staging area.

```
git add -p
```

### 3. Commands for commit 

By commit we push or code from staging area to local repository.

command :

```
git commit -m "enter message here"
```

### 4. Commands for push :

```
git push origin -u <branch-name>
```

'-u' by adding this option you don't need to mention branch-name everytime you push the code . You can just run the command :

```
git push
```

This will push code to repo on the branch on which you previously pushed using -u option



## Contribute with others on github

### Create branch with  :

```
git switch -c <brach-name>
```

This will create a new branch and also switch to that branch

### Change branch :

```
git checkout <branch-name>
```

### To see all branches you have created locally :

```
git branch
```

### Pull the code from remote github 

```
git pull origin <branch-name>
```

like

```
git pull orign main
```

There are 2 ways to contribute in code :

### Rebase and Merge

After running git pull orign main there may be conflicts in code , to resolve conflicts we use rebase and merge.

```
git rebase origin/main
```

This matains commit history sequentially.

Best way to fix merge conflicts is it use 

```
git pull orign main
```
This does 2 things 

```
git  fetch 
```
and 

```
git merge
```

To fix merge conflicts go to particular file where there are merge conflics and make sure only code that is correct is there .


