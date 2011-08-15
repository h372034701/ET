#include <boost/foreach.hpp>
#include "Base/Marcos.h"
#include "Event/OrNode.h"

namespace Egametang {

OrNode::~OrNode()
{
	foreach(NodeIf* node, nodes)
	{
		delete node;
	}
}

bool OrNode::Run(ContexIf* contex)
{
	foreach(NodeIf* node, nodes)
	{
		if (!node->Run(contex))
		{
			return true;
		}
	}
	return false;
}

void OrNode::AddChildNode(NodeIf *node)
{
	nodes.push_back(node);
}

NodeIf* OrNodeFactory::GetInstance(const EventNode& conf)
{
	return new OrNode();
}

} // namespace Egametang
