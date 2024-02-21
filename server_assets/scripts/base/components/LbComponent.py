
import KBEngine

from KBEDebug import DEBUG_MSG

class LbComponent(KBEngine.EntityComponent):

    def __init__(self):
        KBEngine.EntityComponent.__init__(self)
        DEBUG_MSG('LbComponent.__init__')

    def onAttached(self, owner):
        """
        """
        DEBUG_MSG("LbComponent::onAttached(): owner=%i" % (owner.id))
        
    def onDetached(self, owner):
        """
        """
        DEBUG_MSG("LbComponent::onDetached(): owner=%i" % (owner.id))
