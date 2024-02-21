import KBEngine
import KBEDebug as log



# def load_table(md, filed_name):
# 	tb_name = f'tbl_{md.className}_{filed_name}'
# 	sql = f'select * from {tb_name} where parentID = {md.databaseID}'

# 	def callback(result, rows, insertid, error):
# 		if error:
# 			log.ERROR_MSG('select error')
# 		else:
# 			lst = []
# 			for item in result:
# 				dbid = int(item[0])

# 			setattr(md, filed_name, lst)

# 	KBEngine.executeRawDatabaseCommand(sql, callback)


def add_line(md, filed_name):
	tb_name = f'tbl_{md.className}_{filed_name}'
	sql = f'insert into {tb_name} (parentID, sm_p1, sm_p2, sm_name) values({md.databaseID}, {2}, {3}, \"{"abcd"}\");'

	log.DEBUG_MSG(f'add_line {sql}')
	def sqlCallback(result, rows, insertid, error):
		if error:
			log.ERROR_MSG(f'{tb_name}::saveDB error: {error}')
		else:
			log.DEBUG_MSG(f'{tb_name}::saveDB success')

	KBEngine.executeRawDatabaseCommand(sql, sqlCallback)

def del_line(md):
	"""
	将parentID设为0，id默认从1开始，0表示无对应的parent
	"""
	pass


# def update_table(md, lst_name items):
# 	# update tbl_Lobby_avatarDataLst set sm_param1=1,sm_param2=3 where id=1
# 	parent_id = md.databaseID
# 	sql = f'update tbl_{md.className}_{lst_name} set'
# 	for item in items:
# 		sql += 


class Lobby(KBEngine.Entity):

	def __init__(self):
		KBEngine.Entity.__init__(self)

		self.addTimer(0, 10, 11)


	def onTimer(self, tid, userArg):
		if userArg == 11:
			# self.dbVersion += 1
			# log.DEBUG_MSG(f'self.dbVersion: {self.dbVersion}')
			log.DEBUG_MSG('--------------------------')

	def addLst(self, v1, v2):
		self.avatarDataLst.append({
				'param1': v1,
				'param2': v2
			})

	def delLst(self, idx):
		if idx < len(self.avatarDataLst):
			del self.avatarDataLst[idx]

	def changeItem(self, idx, v1, v2):
		self.avatarDataLst[idx]['param1'] = v1
		self.avatarDataLst[idx]['param2'] = v2

	def addItem(self):
		add_line(self, 'items')