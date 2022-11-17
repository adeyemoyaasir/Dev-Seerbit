from flask import Flask, render_template, url_for, request, flash,current_app, redirect
from flaskext.mysql import MySQL
import pymysql.cursors
import os
import datetime


app = Flask(__name__)

app.secret_key = 'mustaphasecret'
app.config['MYSQL_DATABASE_HOST'] = 'localhost'
app.config['MYSQL_DATABASE_DB'] = 'studentportal'
app.config['MYSQL_DATABASE_USER'] = 'root'
app.config['MYSQL_DATABASE_PASSWORD'] = 'KROMUs j ZVW791m M'

mysql = MySQL(app, cursorclass=pymysql.cursors.DictCursor)

app= Flask(__name__)

@app.route('/')
def index():
    conn = mysql.get_db()
    cur = conn.cursor()
    cur.execute('select * from devseerbit')
    rv = cur.fetchall()
    #print(rv)
    return render_template('index.html', words=rv)


@app.route('/register')
def register():
    
    return render_template('register.html')

@app.route('/confirmregister', methods= ['GET', 'POST'])
def confirm_register():
    if request.method == 'POST':
        lname = request.form['lname']
        gender = request.form['gender']
        email = request.form['email']  
        course = request.form['course']
        amount = 2000
        amountpaid = 0
        status = 'Still owing us'


        conn = mysql.get_db()
        cur = conn.cursor()
        cur.execute('insert into studentportal.devseerbit(studentname, gender, email, amount, course, amountpaid, status) VALUES(%s, %s, %s, %s, %s, %s, %s)', (lname, gender, email, amount, course, amountpaid, status))
        conn.commit()
        cur.close()

    return redirect(url_for('index'))

@app.route('/payment/<id>')
def payment(id):
    student_id = id
    conn = mysql.get_db()
    cur = conn.cursor()
    cur.execute('select * from studentportal.devseerbit where id=%s ', (student_id))
    rv = cur.fetchall()
    
    return render_template('payment.html', words= rv)

        


if __name__ == "__main__":
    app.run(debug=True)