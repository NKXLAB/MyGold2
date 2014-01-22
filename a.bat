git init
git add README.md
git commit -m "first commit"
git remote add origin https://github.com/zhangdong1981/MyGold2.git
git push -u origin master

git fetch
git merge
git config branch.master.remote origin 
git config branch.master.merge refs/heads/master 
git remote add origin https://github.com/zhangdong1981/MyGold.git
git push -u origin master
git push -f

git push -u origin master -f 